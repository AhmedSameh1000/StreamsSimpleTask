import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DocumentFileService {
  constructor(private HttpClient: HttpClient) {}

  GetFilesByDocumentId(DocumentId: any) {
    return this.HttpClient.get(
      `https://localhost:7175/api/DocumentFile/GetFileBuDocumentid?DocumentId=${DocumentId}`
    );
  }
  DeleteFile(FileId: any) {
    return this.HttpClient.delete(
      `https://localhost:7175/api/DocumentFile/DeleteDocumentFile?id=${FileId}`
    );
  }
  downloadFile(fileId: any) {
    return this.HttpClient.get(
      `https://localhost:7175/api/DocumentFile/DownloadFile?fileId=${fileId}`,
      { observe: 'response', responseType: 'blob' }
    );
  }
  InsertNewFiles(files: any) {
    return this.HttpClient.post(
      'https://localhost:7175/api/DocumentFile/InsertFileIntoDocument',
      files
    );
  }
}
