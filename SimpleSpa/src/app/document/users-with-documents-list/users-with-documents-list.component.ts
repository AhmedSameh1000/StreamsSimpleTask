import { MatDialog } from '@angular/material/dialog';
import { DocumentService } from './../../Services/Document/document.service';
import { Component, OnInit } from '@angular/core';
import { UserDocumentsComponent } from '../user-documents/user-documents.component';

@Component({
  selector: 'app-users-with-documents-list',
  templateUrl: './users-with-documents-list.component.html',
  styleUrl: './users-with-documents-list.component.css',
})
export class UsersWithDocumentsListComponent implements OnInit {
  constructor(
    private DocumentService: DocumentService,
    private MatDialog: MatDialog
  ) {}
  ngOnInit(): void {
    this.LoadUsersWithDocumens();
  }
  displayedColumns: string[] = ['userName', 'email', 'actions'];

  UserDocuments: [];
  LoadUsersWithDocumens() {
    this.DocumentService.GetUsersWithHisDocuments().subscribe({
      next: (res: any) => {
        this.UserDocuments = res;
        console.log(res);
      },
    });
  }
  ShowUserDocuments(userId, email) {
    this.MatDialog.open(UserDocumentsComponent, {
      minWidth: '50%',
      data: {
        id: userId,
        email: email,
      },
    });
  }
}
