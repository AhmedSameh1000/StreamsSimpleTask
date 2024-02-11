import { PriortyService } from './../../Services/Priorty/priorty.service';
import { DocumentService } from './../../Services/Document/document.service';
import { AuthService } from './../../Services/Auth/auth.service';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { formatDate } from '@angular/common';
import { AllowedExtensionsValidator } from '../../Validators/AllowedExtensions.validator';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html',
  styleUrl: './create-document.component.css',
})
export class CreateDocumentComponent implements OnInit {
  constructor(
    private AuthService: AuthService,
    private DocumentService: DocumentService,
    private PriortyService: PriortyService,
    private MatDialogref: MatDialogRef<CreateDocumentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { id: any }
  ) {}
  ngOnInit(): void {
    this.LoadPriorties();
    this.InitializeForm();
    if (this.data != null) {
      this.LoadSingleDocument();
    }
    console.log(this.data);
  }
  DocumentForm: FormGroup;

  InitializeForm() {
    this.DocumentForm = new FormGroup({
      Name: new FormControl('', [Validators.required]),
      UserId: new FormControl(this.AuthService.GetUserId()),
      Due_date: new FormControl([Validators.required]),
      priorityId: new FormControl([Validators.required]),
      files: new FormArray([]),
    });
  }
  SaveDocument() {
    var arr = this.DocumentForm.get('files') as FormArray;
    if (this.DocumentForm.invalid || arr.length === 0) return;
    this.DocumentService.CreateDocument(this.GetFormData()).subscribe({
      next: (res) => {
        console.log(res);
        this.MatDialogref.close(true);
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
        var arr = this.DocumentForm.get('files') as FormArray;
        arr.clear();
      },
    });
  }

  LoadSingleDocument() {
    this.DocumentService.GetSingleDocument(this.data).subscribe({
      next: (res: any) => {
        console.log(res);
        this.DocumentForm.patchValue({
          Name: res.name,
          UserId: this.AuthService.GetUserId(),
          Due_date: res.dueDate,
          priorityId: res.priortyId,
        });
      },
    });
  }

  GetFormData(): FormData {
    var date = new Date(this.DocumentForm.get('Due_date').value).getFullYear();

    var DocumentModel = new FormData();
    DocumentModel.append('Name', this.DocumentForm.get('Name').value);
    DocumentModel.append('UserId', this.AuthService.GetUserId());

    const dueDate = this.DocumentForm.get('Due_date').value;
    const formattedDueDate = formatDate(
      dueDate,
      'yyyy-MM-ddTHH:mm:ss.SSSZ',
      'en-US'
    );
    DocumentModel.append('Due_date', formattedDueDate);
    DocumentModel.append(
      'PriorityId',
      this.DocumentForm.get('priorityId').value
    );

    if (this.DocumentForm.get('files').value.length > 0) {
      const filesArray = this.DocumentForm.get('files').value;
      for (let i = 0; i < filesArray.length; i++) {
        DocumentModel.append('files', filesArray[i]);
      }
    }
    return DocumentModel;
  }

  Priorties: any[];
  LoadPriorties() {
    this.PriortyService.GetPriorties().subscribe({
      next: (res: any) => {
        this.Priorties = res;
        console.log(res);
      },
    });
  }

  SelectFiles($event: any) {
    const files: FileList = $event.target.files;

    const filesFormArray = this.DocumentForm.get('files') as FormArray;

    for (let i = 0; i < files.length; i++) {
      filesFormArray.push(new FormControl(files[i]));
    }
  }
  UpdateDocument() {
    this.DocumentService.UpdateDocument(
      this.GetFormData(),
      this.data
    ).subscribe({
      next: (res) => {
        this.MatDialogref.close(true);
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        });
        var arr = this.DocumentForm.get('files') as FormArray;
        arr.clear();
      },
    });
  }
}
