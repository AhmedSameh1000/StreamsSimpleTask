import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DocumentService {
  constructor(private HttpClient: HttpClient) {}

  GetUserDocuments(UserId: string) {
    return this.HttpClient.get(
      `https://localhost:7175/api/Document/GetUserDocuments?UserId=${UserId}`
    );
  }
}
