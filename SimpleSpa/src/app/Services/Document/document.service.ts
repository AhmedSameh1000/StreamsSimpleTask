import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DocumentService {
  constructor(private HttpClient: HttpClient) {}

  GetSingleDocument(DocumentId: any) {
    return this.HttpClient.get(
      `https://localhost:7175/api/Document/GetSingleDocument?documentId=${DocumentId}`
    );
  }
  GetUserDocuments(UserId: string) {
    return this.HttpClient.get(
      `https://localhost:7175/api/Document/GetUserDocuments?UserId=${UserId}`
    );
  }

  GetUsersWithHisDocuments() {
    return this.HttpClient.get(
      `https://localhost:7175/api/Document/GetUserWithHisDocuments`
    );
  }
  CreateDocument(Document: any) {
    return this.HttpClient.post(
      'https://localhost:7175/api/Document/CreateDocument',
      Document
    );
  }
  UpdateDocument(Document: any, DocumentId: any) {
    return this.HttpClient.put(
      `https://localhost:7175/api/Document/UpdateDocument?documentId=${DocumentId}`,
      Document
    );
  }
  DeleteDocument(documentId: any, UserId: string) {
    return this.HttpClient.delete(
      `https://localhost:7175/api/Document/DeleteDocument?documentId=${documentId}&UserId=${UserId}`
    );
  }
}
