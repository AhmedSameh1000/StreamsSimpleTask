import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DocumentRoutingModule } from './document-routing.module';
import { DocumentListComponent } from './document-list/document-list.component';
import { MaterialModule } from '../material/material.module';

@NgModule({
  declarations: [DocumentListComponent],
  imports: [CommonModule, DocumentRoutingModule, MaterialModule],
})
export class DocumentModule {}
