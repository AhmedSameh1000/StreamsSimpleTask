import { MatDialog } from '@angular/material/dialog';
import { AuthService } from './../../Services/Auth/auth.service';
import { DocumentService } from './../../Services/Document/document.service';
import { Component, OnInit, createComponent } from '@angular/core';
import { CreateDocumentComponent } from '../create-document/create-document.component';
import Swal from 'sweetalert2';
import { DocumentFileDetailsComponent } from '../document-file-details/document-file-details.component';

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
    private AuthService: AuthService,
    private MatDilog: MatDialog
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
    'files',
    'actions',
  ];
  OpenModel() {
    let dialogRef = this.MatDilog.open(CreateDocumentComponent, {
      minWidth: '50%',
    });
    dialogRef.afterClosed().subscribe({
      next: (res: any) => {
        if (res) {
          this.LoadDocuments();
        }
      },
    });
  }
  DeleteDocuemnt(id) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.DocumentService.DeleteDocument(
          id,
          this.AuthService.GetUserId()
        ).subscribe({
          next: (res) => {
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
              position: 'top-left',
              width: '40%',
              heightAuto: true,
            });
            this.LoadDocuments();
          },
        });
      }
    });
  }

  OpenModelToUpdate(id: any) {
    let dialogRef = this.MatDilog.open(CreateDocumentComponent, {
      minWidth: '50%',
      data: id,
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        if (res) {
          this.LoadDocuments();
        }
      },
    });
  }

  OpenModelForDisplay(id) {
    console.log(id);
    let AfterClose = this.MatDilog.open(DocumentFileDetailsComponent, {
      minWidth: '30%',
      data: {
        id: id,
        Name: this.AuthService.GetName(),
        Email: this.AuthService.GetEmail(),
      },
    });
    AfterClose.afterClosed().subscribe({
      next: (res) => {
        if (res) {
          this.LoadDocuments();
        }
      },
    });
  }
}
