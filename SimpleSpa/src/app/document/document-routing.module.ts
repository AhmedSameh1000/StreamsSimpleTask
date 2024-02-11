import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DocumentListComponent } from './document-list/document-list.component';
import { UsersWithDocumentsListComponent } from './users-with-documents-list/users-with-documents-list.component';

const routes: Routes = [
  {
    path: 'list',
    component: DocumentListComponent,
  },
  {
    path: '',
    component: DocumentListComponent,
  },
  {
    path: 'userswithdocumens',
    component: UsersWithDocumentsListComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DocumentRoutingModule {}
