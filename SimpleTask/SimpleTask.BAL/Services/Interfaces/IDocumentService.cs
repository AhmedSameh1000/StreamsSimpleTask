using SimpleTask.BAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTask.BAL.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<bool> CreateDocumentAsync(DocumentForCreateDTo document);

        Task<bool> UpdateDocumentAsync(DocumentForCreateDTo document, int documentId);

        Task<bool> DeleteDocument(int DocumentId, string UserId);
    }
}