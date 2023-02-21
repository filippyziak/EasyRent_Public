/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RentalAdService } from './rental-ad.service';

describe('Service: RentalAd', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RentalAdService]
    });
  });

  it('should ...', inject([RentalAdService], (service: RentalAdService) => {
    expect(service).toBeTruthy();
  }));
});
