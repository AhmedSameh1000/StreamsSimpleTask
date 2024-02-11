import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersWithDocumentsListComponent } from './users-with-documents-list.component';

describe('UsersWithDocumentsListComponent', () => {
  let component: UsersWithDocumentsListComponent;
  let fixture: ComponentFixture<UsersWithDocumentsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UsersWithDocumentsListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UsersWithDocumentsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
