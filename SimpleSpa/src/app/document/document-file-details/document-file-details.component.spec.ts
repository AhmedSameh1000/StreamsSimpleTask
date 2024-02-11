import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentFileDetailsComponent } from './document-file-details.component';

describe('DocumentFileDetailsComponent', () => {
  let component: DocumentFileDetailsComponent;
  let fixture: ComponentFixture<DocumentFileDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DocumentFileDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocumentFileDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
