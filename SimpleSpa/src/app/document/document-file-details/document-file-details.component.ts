import {
  Component,
  Inject,
  OnInit,
  ɵIS_HYDRATION_DOM_REUSE_ENABLED,
} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DocumentFileService } from '../../Services/DocumentFile/document-file.service';
import Swal from 'sweetalert2';
import { tick } from '@angular/core/testing';
import { TitleStrategy } from '@angular/router';
import { FormArray, FormControl, Validators } from '@angular/forms';
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
    console.log(this.data);
    this.LoadDocumentsFile();
  }
  Files: any[];
  LoadDocumentsFile() {
    this.DocumentFileService.GetFilesByDocumentId(this.data.id).subscribe({
      next: (res: any) => {
        this.Files = res;
        console.log(res);
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
  InsertNewFiles($event) {
    const selectedfiles: any = $event.target.files;

    var formdata = new FormData();
    formdata.append('DocumentId', this.data.id);

    for (let i = 0; i < selectedfiles.length; i++) {
      this.SelectFormFiles.push(new FormControl(selectedfiles[i]));
      formdata.append('files', selectedfiles[i]);
    }
    this.DocumentFileService.InsertNewFiles(formdata).subscribe({
      next: (res) => {
        this.LoadDocumentsFile();
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
