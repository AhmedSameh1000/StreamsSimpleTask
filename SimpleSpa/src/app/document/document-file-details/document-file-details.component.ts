import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DocumentFileService } from '../../Services/DocumentFile/document-file.service';
import Swal from 'sweetalert2';
import { FormArray, FormControl, Validators } from '@angular/forms';
import { AllowedExtensionsValidator } from '../../Validators/AllowedExtensions.validator';

@Component({
  selector: 'app-document-file-details',
  templateUrl: './document-file-details.component.html',
  styleUrl: './document-file-details.component.css',
})
export class DocumentFileDetailsComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { id: any; Name: any; Email: any },
    private DocumentFileService: DocumentFileService,
    private MatDialogRef: MatDialogRef<DocumentFileDetailsComponent>
  ) {}
  ngOnInit(): void {
    this.LoadDocumentsFile();
  }
  Files: any[];
  LoadDocumentsFile() {
    this.DocumentFileService.GetFilesByDocumentId(this.data.id).subscribe({
      next: (res: any) => {
        this.Files = res;
      },
    });
  }

  GetType(filePath: string) {
    return filePath.split('.')[1];
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
            this.LoadDocumentsFile();
          },
        });
      }
    });
  }
  CloseDilog() {
    this.MatDialogRef.close(true);
  }
  downloadFile(id, fileName) {
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

  SelectFormFiles = new FormArray([]);
  SelectFiles($event) {
    const selectedfiles: any = $event.target.files;

    var formdata = new FormData();
    for (let i = 0; i < selectedfiles.length; i++) {
      this.SelectFormFiles.push(
        new FormControl(selectedfiles[i], [AllowedExtensionsValidator])
      );
      formdata.append('files', selectedfiles[i]);
    }

    if (this.SelectFormFiles.invalid) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please Select Valid Files',
      });
      this.SelectFormFiles.clear();
      return;
    }

    formdata.append('DocumentId', this.data.id);

    this.InsertFiles(formdata);
    this.SelectFormFiles.clear();
  }
  InsertFiles(formdata: any) {
    this.DocumentFileService.InsertNewFiles(formdata).subscribe({
      next: (res) => {
        this.LoadDocumentsFile();
      },
      error: (err) => {},
    });
  }
}
