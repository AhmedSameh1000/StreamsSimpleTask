﻿using SimpleTask.BAL.DTOs;
namespace SimpleTask.BAL.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<bool> CreateDocumentAsync(DocumentForCreateDTo document);

        Task<bool> UpdateDocumentAsync(DocumentForCreateDTo document, int documentId);

        Task<bool> DeleteDocument(int DocumentId, string UserId);

        Task<List<DocumentForReturnDTO>> GetUserDocuments(string UserId);
    }



}