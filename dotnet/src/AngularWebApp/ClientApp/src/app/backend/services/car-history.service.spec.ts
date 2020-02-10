import { TestBed } from '@angular/core/testing';

import { CarHistoryService } from './car-history.service';

describe('CarHistoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CarHistoryService = TestBed.get(CarHistoryService);
    expect(service).toBeTruthy();
  });
});
