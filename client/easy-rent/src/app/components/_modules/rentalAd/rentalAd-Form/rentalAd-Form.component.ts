import { Router } from '@angular/router';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { RentalAdService } from './../../../../services/rental-ad.service';
import { CurrencyPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RentalAdValidationRules } from 'src/app/validations/rentalAdValidationRules';

@Component({
  selector: 'app-rentalAd-Form',
  templateUrl: './rentalAd-Form.component.html',
  styleUrls: ['./rentalAd-Form.component.scss'],
})
export class RentalAdFormComponent implements OnInit {
  photos: string[] = [];
  uploads: FileList;
  createForm: FormGroup;
  validationConsts = RentalAdValidationRules;

  constructor(private rentalAdService: RentalAdService) {}

  ngOnInit() {
    this.createForm = new FormGroup({
      title: new FormControl('', [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.TitleMinLength),
        Validators.maxLength(RentalAdValidationRules.TitleMaxLength),
      ]),
      description: new FormControl('', [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.DescriptionMinLength),
        Validators.maxLength(RentalAdValidationRules.DescriptionMaxLength),
      ]),
      country: new FormControl('', [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.PlaceAddressMinLength),
        Validators.maxLength(RentalAdValidationRules.PlaceAddressMaxLength),
      ]),
      city: new FormControl('', [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.PlaceAddressMinLength),
        Validators.maxLength(RentalAdValidationRules.PlaceAddressMaxLength),
      ]),
      street: new FormControl('', [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.PlaceAddressMinLength),
        Validators.maxLength(RentalAdValidationRules.PlaceAddressMaxLength),
      ]),
      price: new FormControl('', [Validators.required,
      Validators.min(RentalAdValidationRules.PricePerDayMinPrice),
      Validators.max(RentalAdValidationRules.PricePerDayMaxPrice)]),
      file: new FormControl(''),
      fileSource: new FormControl(''),
    });
  }

  get title() { return this.createForm.get('title'); }
  get description() { return this.createForm.get('description'); }
  get country() { return this.createForm.get('country'); }
  get city() { return this.createForm.get('city'); }
  get street() { return this.createForm.get('street'); }
  get price() { return this.createForm.get('price'); }

  public createRentalAd(form: FormGroup){
    console.log(this.uploads);
    this.rentalAdService.createRentalAd(form.value.description,
      form.value.title,
      form.value.price,
      form.value.country,
      form.value.city,
      form.value.street,
      typeof this.uploads !== 'undefined' ? Array.from(this.uploads) : []);
  }

  public removeFile(photoIndex: number) {
    const dt = new DataTransfer();

    for (let i = 0; i < this.uploads.length; i++) {
      const file = this.uploads[i];
      if (photoIndex !== i) dt.items.add(file); // here you exclude the file. thus removing it.
    }

    this.uploads = dt.files; // Assign the updates list
    console.log(this.uploads);

    if (Array.isArray(this.photos)) {
      this.photos.splice(photoIndex, 1);
    }
  }

  get f() {
    return this.createForm.controls;
  }

  onFileChange(event: any) {
    if (event.target.files && event.target.files[0]) {
      this.uploads = event.target.files;
      var filesAmount = event.target.files.length;
      this.photos = [];
      for (let i = 0; i < filesAmount; i++) {
        var reader = new FileReader();

        reader.onload = (event: any) => {
          this.photos.push(event.target.result);

          this.createForm.patchValue({
            fileSource: this.photos,
          });
        };

        reader.readAsDataURL(event.target.files[i]);
      }
    }
  }
}
