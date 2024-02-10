import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogInComponent } from '../auth/log-in/log-in.component';
import { SignUpComponent } from '../auth/sign-up/sign-up.component';

const routes: Routes = [
  {
    path: 'signup',
    component: SignUpComponent,
  },
  {
    path: 'login',
    component: LogInComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CoreRoutingModule {}
