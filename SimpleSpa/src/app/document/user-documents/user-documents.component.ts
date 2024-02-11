import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DocumentFileService } from './../../Services/DocumentFile/document-file.service';
import { Component, Inject, OnInit } from '@angular/core';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-documents',
  templateUrl: './user-documents.component.html',
  styleUrl: './user-documents.component.css',
})
export class UserDocumentsComponent implements OnInit {
  constructor(
    private DocumentFileService: DocumentFileService,
    @Inject(MAT_DIALOG_DATA) public data: { id: any; email: any }
  ) {}
  ngOnInit(): void {
    this.LoadDocumensFilebyUserid();
  }

  DeleteFile(id) {
    Swal.fire({
      text: "You won't be able to revert this!",
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'delete',
      width: '20%',
    }).then((result) => {
      if (result.isConfirmed) {
        this.DocumentFileService.DeleteFile(id).subscribe({
          next: (res) => {
            this.LoadDocumensFilebyUserid();
          },
        });
      }
    });
  }
  DownloadFile(id, fileName) {
    this.DocumentFileService.downloadFile(id).subscribe({
      next: (response) => {
        let blob = response.body as Blob;
        let a = document.createElement('a');
        a.download = fileName;
        a.href = window.URL.createObjectURL(blob);
        a.click();
      },
    });
  }
  Files: any[];
  LoadDocumensFilebyUserid() {
    this.DocumentFileService.GetFilesByUserId(this.data.id).subscribe({
      next: (res: any) => {
        this.Files = res;
        console.log(res);
      },
    });
  }
  GetType(filePath: string) {
    return filePath.split('.')[1];
  }
}
