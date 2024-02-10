import { TestBed } from '@angular/core/testing';

import { PriortyService } from './priorty.service';

describe('PriortyService', () => {
  let service: PriortyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PriortyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
