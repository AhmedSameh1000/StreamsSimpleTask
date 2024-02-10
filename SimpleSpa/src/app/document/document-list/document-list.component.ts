import { AuthService } from './../../Services/Auth/auth.service';
import { DocumentService } from './../../Services/Document/document.service';
import { Component, OnInit } from '@angular/core';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

@Component({
  selector: 'app-document-list',
  templateUrl: './document-list.component.html',
  styleUrl: './document-list.component.css',
})
export class DocumentListComponent implements OnInit {
  constructor(
    private DocumentService: DocumentService,
    private AuthService: AuthService
  ) {}
  ngOnInit(): void {
    this.LoadDocuments();
  }

  Documents: any[];
  LoadDocuments() {
    this.DocumentService.GetUserDocuments(
      this.AuthService.GetUserId()
    ).subscribe({
      next: (res: any) => {
        console.log(res);
        this.Documents = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
  displayedColumns: string[] = [
    'name',
    'priorty',
    'email',
    'userName',
    'createdDate',
    'dueDate',
    'actions',
  ];
}
