using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Moq;
using SimpleTask.BAL.DTOs;
using SimpleTask.BAL.Services.Implementation;
using SimpleTask.BAL.Services.Interfaces;
using SimpleTask.DAL.Domains;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;

namespace SimpleTask.UnitTest
{
    public class DocumentServiceTest
    {
        private readonly IDocumentRepository _DocumentRepository;
        private readonly IDocumentService _DocumentService;
        private readonly Mock<IDocumentRepository> _DocumentRepositryMock;

        private readonly Mock<IFileServices> _fileServicesMock;
        private readonly Mock<IWebHostEnvironment> _webHostMock;
        private readonly Mock<IUserRepository> _UserRepoMock;

        public DocumentServiceTest()

        {
            _fileServicesMock = new Mock<IFileServices>();
            _webHostMock = new Mock<IWebHostEnvironment>();
            _UserRepoMock = new Mock<IUserRepository>();
            _DocumentRepositryMock = new Mock<IDocumentRepository>();

            _DocumentRepository = _DocumentRepositryMock.Object;

            _DocumentService = new DocumentService(_DocumentRepository, _fileServicesMock.Object, _webHostMock.Object, _UserRepoMock.Object);
        }

        #region CreateDocumentAsync

        [Fact]
        public async Task Test_CreateDocumentAsync_DocumentIsNull_ReturnFalse()
        {
            // إعداد المتغيرات المطلوبة
            DocumentForCreateDTo documentModel = null;
            var Result = await _DocumentService.CreateDocumentAsync(documentModel);

            Result.Should().BeFalse();
        }

        [Fact]
        public async Task Test_CreateDocumentAsync_DocumentArgumentIsNull_ReturnFalse()
        {
            DocumentForCreateDTo documentModel = new DocumentForCreateDTo()
            {
            };

            _DocumentRepositryMock.Setup(p => p.Add(It.IsAny<Document>()));

            var Result = await _DocumentService.CreateDocumentAsync(documentModel);

            Result.Should().BeFalse();
        }

        #endregion CreateDocumentAsync

        #region UpdateDocument

        [Fact]
        public async Task Test_UpdateDocument_Model_is_Null()
        {
            DocumentForCreateDTo documentModel = null;

            var Result = await _DocumentService.UpdateDocumentAsync(documentModel, 10);

            Result.Should().BeFalse();
        }

        [Fact]
        public async Task Test_UpdateDocument_ModelArguments_is_Null()
        {
            DocumentForCreateDTo documentModel = new DocumentForCreateDTo()
            {
            };
            var Result = await _DocumentService.UpdateDocumentAsync(documentModel, 10);

            Result.Should().BeFalse();
        }

        #endregion UpdateDocument
    }
}