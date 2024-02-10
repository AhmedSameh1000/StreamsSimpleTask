import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { MaterialModule } from '../material/material.module';

@NgModule({
  declarations: [NavBarComponent],
  imports: [CommonModule, CoreRoutingModule, MaterialModule],
  exports: [NavBarComponent],
})
export class CoreModule {}
