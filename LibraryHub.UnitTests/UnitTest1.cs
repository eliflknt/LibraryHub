using FluentAssertions;
using LibraryHub.Application.Interfaces;
using LibraryHub.Application.Services;
using LibraryHub.Domain.Entities;
using LibraryHub.Domain.Enums;
using Moq;

namespace LibraryHub.UnitTests;

public class LoanServiceTests
{
    [Fact]
    public async Task BorrowBookAsync_ShouldFail_WhenMemberHasUnpaidFine()
    {
        // Arrange
        var loanRepository = new Mock<IGenericRepository<Loan>>();
        var memberRepository = new Mock<IGenericRepository<Member>>();
        var bookRepository = new Mock<IGenericRepository<Book>>();
        var fineRepository = new Mock<IGenericRepository<Fine>>();

        var member = new Member
        {
            Id = 1,
            FullName = "Test Üye",
            Email = "test@test.com",
            IsActive = true
        };

        var book = new Book
        {
            Id = 1,
            Title = "Test Kitap",
            RaftakiAdet = 1
        };

        var existingLoan = new Loan
        {
            Id = 10,
            MemberId = 1,
            BookId = 2,
            Status = LoanStatus.Returned
        };

        var unpaidFine = new Fine
        {
            Id = 1,
            LoanId = 10,
            Amount = 10m,
            IsPaid = false
        };

        memberRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(member);

        bookRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(book);

        loanRepository
            .Setup(x => x.FindAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Loan, bool>>>()))
            .ReturnsAsync(new List<Loan> { existingLoan });

        fineRepository
            .Setup(x => x.FindAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Fine, bool>>>()))
            .ReturnsAsync(new List<Fine> { unpaidFine });

        var service = new LoanService(
            loanRepository.Object,
            memberRepository.Object,
            bookRepository.Object,
            fineRepository.Object);

        // Act
        var result = await service.BorrowBookAsync(1, 1);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(
            "Ödenmemiş cezası bulunan üye yeni kitap alamaz.");

        book.RaftakiAdet.Should().Be(1);

        loanRepository.Verify(
            x => x.AddAsync(It.IsAny<Loan>()),
            Times.Never);
    }

    [Fact]
    public async Task BorrowBookAsync_ShouldFail_WhenMemberHasThreeActiveLoans()
    {
        // Arrange
        var loanRepository = new Mock<IGenericRepository<Loan>>();
        var memberRepository = new Mock<IGenericRepository<Member>>();
        var bookRepository = new Mock<IGenericRepository<Book>>();
        var fineRepository = new Mock<IGenericRepository<Fine>>();

        var member = new Member
        {
            Id = 1,
            FullName = "Test Üye",
            Email = "test@test.com",
            IsActive = true
        };

        var book = new Book
        {
            Id = 1,
            Title = "Dördüncü Kitap",
            RaftakiAdet = 1
        };

        var activeLoans = new List<Loan>
        {
            new Loan
            {
                Id = 1,
                MemberId = 1,
                BookId = 10,
                Status = LoanStatus.Active
            },
            new Loan
            {
                Id = 2,
                MemberId = 1,
                BookId = 11,
                Status = LoanStatus.Active
            },
            new Loan
            {
                Id = 3,
                MemberId = 1,
                BookId = 12,
                Status = LoanStatus.Active
            }
        };

        memberRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(member);

        bookRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(book);

        loanRepository
            .Setup(x => x.FindAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Loan, bool>>>()))
            .ReturnsAsync(activeLoans);

        var service = new LoanService(
            loanRepository.Object,
            memberRepository.Object,
            bookRepository.Object,
            fineRepository.Object);

        // Act
        var result = await service.BorrowBookAsync(1, 1);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(
            "Üye aynı anda en fazla 3 kitap ödünç alabilir.");

        book.RaftakiAdet.Should().Be(1);

        loanRepository.Verify(
            x => x.AddAsync(It.IsAny<Loan>()),
            Times.Never);
    }

    [Fact]
    public async Task BorrowBookAsync_ShouldFail_WhenBookIsOutOfStock()
    {
        // Arrange
        var loanRepository = new Mock<IGenericRepository<Loan>>();
        var memberRepository = new Mock<IGenericRepository<Member>>();
        var bookRepository = new Mock<IGenericRepository<Book>>();
        var fineRepository = new Mock<IGenericRepository<Fine>>();

        var member = new Member
        {
            Id = 1,
            FullName = "Test Üye",
            Email = "test@test.com",
            IsActive = true
        };

        var book = new Book
        {
            Id = 1,
            Title = "Stokta Olmayan Kitap",
            RaftakiAdet = 0
        };

        memberRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(member);

        bookRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(book);

        var service = new LoanService(
            loanRepository.Object,
            memberRepository.Object,
            bookRepository.Object,
            fineRepository.Object);

        // Act
        var result = await service.BorrowBookAsync(1, 1);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Kitap rafta bulunmuyor.");

        loanRepository.Verify(
            x => x.AddAsync(It.IsAny<Loan>()),
            Times.Never);

        book.RaftakiAdet.Should().Be(0);
    }

    [Fact]
    public async Task BorrowBookAsync_ShouldFail_WhenMemberIsInactive()
    {
        // Arrange
        var loanRepository = new Mock<IGenericRepository<Loan>>();
        var memberRepository = new Mock<IGenericRepository<Member>>();
        var bookRepository = new Mock<IGenericRepository<Book>>();
        var fineRepository = new Mock<IGenericRepository<Fine>>();

        var member = new Member
        {
            Id = 1,
            FullName = "Pasif Üye",
            Email = "pasif@test.com",
            IsActive = false
        };

        memberRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(member);

        var service = new LoanService(
            loanRepository.Object,
            memberRepository.Object,
            bookRepository.Object,
            fineRepository.Object);

        // Act
        var result = await service.BorrowBookAsync(1, 1);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Üye aktif değil.");

        bookRepository.Verify(
            x => x.GetByIdAsync(It.IsAny<int>()),
            Times.Never);

        loanRepository.Verify(
            x => x.AddAsync(It.IsAny<Loan>()),
            Times.Never);
    }

    [Fact]
    public async Task BorrowBookAsync_ShouldSucceed_WhenMemberCanBorrowBook()
    {
        // Arrange
        var loanRepository = new Mock<IGenericRepository<Loan>>();
        var memberRepository = new Mock<IGenericRepository<Member>>();
        var bookRepository = new Mock<IGenericRepository<Book>>();
        var fineRepository = new Mock<IGenericRepository<Fine>>();

        var member = new Member
        {
            Id = 1,
            FullName = "Aktif Üye",
            Email = "aktif@test.com",
            IsActive = true
        };

        var book = new Book
        {
            Id = 1,
            Title = "Test Kitap",
            RaftakiAdet = 2
        };

        memberRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(member);

        bookRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(book);

        loanRepository
            .Setup(x => x.FindAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Loan, bool>>>()))
            .ReturnsAsync(new List<Loan>());

        loanRepository
            .Setup(x => x.AddAsync(It.IsAny<Loan>()))
            .Callback<Loan>(loan => loan.Id = 1)
            .Returns(Task.CompletedTask);

        var service = new LoanService(
            loanRepository.Object,
            memberRepository.Object,
            bookRepository.Object,
            fineRepository.Object);

        // Act
        var result = await service.BorrowBookAsync(1, 1);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.MemberId.Should().Be(1);
        result.Data.BookId.Should().Be(1);
        result.Data.Status.Should().Be(LoanStatus.Active);

        book.RaftakiAdet.Should().Be(1);

        loanRepository.Verify(
            x => x.AddAsync(It.IsAny<Loan>()),
            Times.Once);

        bookRepository.Verify(
            x => x.Update(book),
            Times.Once);

        loanRepository.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task ReturnBookAsync_ShouldSucceed_WhenBookIsReturnedOnTime()
    {
        // Arrange
        var loanRepository = new Mock<IGenericRepository<Loan>>();
        var memberRepository = new Mock<IGenericRepository<Member>>();
        var bookRepository = new Mock<IGenericRepository<Book>>();
        var fineRepository = new Mock<IGenericRepository<Fine>>();

        var loan = new Loan
        {
            Id = 1,
            MemberId = 1,
            BookId = 1,
            LoanDate = DateTime.UtcNow.Date.AddDays(-7),
            DueDate = DateTime.UtcNow.Date.AddDays(7),
            Status = LoanStatus.Active
        };

        var book = new Book
        {
            Id = 1,
            Title = "Test Kitap",
            RaftakiAdet = 0
        };

        loanRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(loan);

        bookRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(book);

        var service = new LoanService(
            loanRepository.Object,
            memberRepository.Object,
            bookRepository.Object,
            fineRepository.Object);

        // Act
        var result = await service.ReturnBookAsync(1);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Status.Should().Be(LoanStatus.Returned);
        result.Data.ReturnDate.Should().NotBeNull();
        result.Data.FineAmount.Should().BeNull();

        book.RaftakiAdet.Should().Be(1);

        fineRepository.Verify(
            x => x.AddAsync(It.IsAny<Fine>()),
            Times.Never);

        loanRepository.Verify(
            x => x.Update(loan),
            Times.Once);

        bookRepository.Verify(
            x => x.Update(book),
            Times.Once);

        loanRepository.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task ReturnBookAsync_ShouldCalculateCorrectFine_WhenBookIsReturnedLate()
    {
        // Arrange
        var loanRepository = new Mock<IGenericRepository<Loan>>();
        var memberRepository = new Mock<IGenericRepository<Member>>();
        var bookRepository = new Mock<IGenericRepository<Book>>();
        var fineRepository = new Mock<IGenericRepository<Fine>>();

        var today = DateTime.UtcNow.Date;

        var loan = new Loan
        {
            Id = 1,
            MemberId = 1,
            BookId = 1,
            LoanDate = today.AddDays(-20),
            DueDate = today.AddDays(-5),
            Status = LoanStatus.Active
        };

        var book = new Book
        {
            Id = 1,
            Title = "Gecikmiş Kitap",
            RaftakiAdet = 0
        };

        loanRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(loan);

        bookRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(book);

        fineRepository
            .Setup(x => x.AddAsync(It.IsAny<Fine>()))
            .Returns(Task.CompletedTask);

        var service = new LoanService(
            loanRepository.Object,
            memberRepository.Object,
            bookRepository.Object,
            fineRepository.Object);

        // Act
        var result = await service.ReturnBookAsync(1);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.FineAmount.Should().Be(10m);

        loan.Status.Should().Be(LoanStatus.Returned);
        loan.ReturnDate.Should().Be(today);

        book.RaftakiAdet.Should().Be(1);

        fineRepository.Verify(
            x => x.AddAsync(It.Is<Fine>(f =>
                f.LoanId == 1 &&
                f.Amount == 10m &&
                f.IsPaid == false)),
            Times.Once);

        loanRepository.Verify(
            x => x.Update(loan),
            Times.Once);

        bookRepository.Verify(
            x => x.Update(book),
            Times.Once);

        loanRepository.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task BorrowBookAsync_ShouldFail_WhenMemberDoesNotExist()
    {
        // Arrange
        var loanRepository = new Mock<IGenericRepository<Loan>>();
        var memberRepository = new Mock<IGenericRepository<Member>>();
        var bookRepository = new Mock<IGenericRepository<Book>>();
        var fineRepository = new Mock<IGenericRepository<Fine>>();

        memberRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Member?)null);

        var service = new LoanService(
            loanRepository.Object,
            memberRepository.Object,
            bookRepository.Object,
            fineRepository.Object);

        // Act
        var result = await service.BorrowBookAsync(1, 1);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Üye bulunamadı.");

        bookRepository.Verify(
            x => x.GetByIdAsync(It.IsAny<int>()),
            Times.Never);

        loanRepository.Verify(
            x => x.AddAsync(It.IsAny<Loan>()),
            Times.Never);
    }
}