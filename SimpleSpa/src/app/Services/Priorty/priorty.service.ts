import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PriortyService {
  constructor(private HttpClient: HttpClient) {}
  GetPriorties() {
    return this.HttpClient.get(
      'https://localhost:7175/api/Priority/GetPriorities'
    );
  }
}
