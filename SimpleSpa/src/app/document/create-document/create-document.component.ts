import { PriortyService } from './../../Services/Priorty/priorty.service';
import { DocumentService } from './../../Services/Document/document.service';
import { AuthService } from './../../Services/Auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { formatDate } from '@angular/common';

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
    private MatDialogref: MatDialogRef<CreateDocumentComponent>
  ) {}
  ngOnInit(): void {
    this.LoadPriorties();
    this.InitializeForm();
  }
  DocumentForm: FormGroup;

  InitializeForm() {
    this.DocumentForm = new FormGroup({
      Name: new FormControl(),
      UserId: new FormControl(this.AuthService.GetUserId()),
      Due_date: new FormControl(),
      priorityId: new FormControl(),
      files: new FormArray([]),
    });
  }
  SaveDocument() {
    this.DocumentService.CreateDocument(this.GetFormData()).subscribe({
      next: (res) => {
        console.log(res);
        this.MatDialogref.close(true);
      },
      error: (err) => {
        console.log(err);
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
    console.log(files);

    const filesFormArray = this.DocumentForm.get('files') as FormArray;

    filesFormArray.clear();

    for (let i = 0; i < files.length; i++) {
      filesFormArray.push(new FormControl(files[i]));
    }
  }
}
