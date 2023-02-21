import { PlaceAddress } from './../../../../models/RentalAdDetails';
import { RentalAdService } from './../../../../services/rental-ad.service';
import { IdentityService } from './../../../../services/identity.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import {
  Form,
  FormControl,
  FormGroup,
  NgForm,
  Validators,
} from '@angular/forms';
import { RentalAdValidationRules } from 'src/app/validations/rentalAdValidationRules';

@Component({
  selector: 'app-rentalAd-edit',
  templateUrl: './rentalAd-edit.component.html',
  styleUrls: ['./rentalAd-edit.component.scss'],
})
export class RentalAdEditComponent implements OnInit {
  public rentalAd: any;
  photos: string[] = [];
  uploads: FileList;
  updateForm: FormGroup;
  existingPhotos: any;
  validationConsts = RentalAdValidationRules;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private rentalAdService: RentalAdService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.rentalAd = data.rentalAdResolver.rentalAd;
      this.existingPhotos = this.rentalAd.placePictures;
      console.log(this.rentalAd);
    });

    if(this.rentalAd.state == 'Archived')
    {
      this.router.navigate(['/browser']);
    }

    this.updateForm = new FormGroup({
      title: new FormControl(this.rentalAd.title, [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.TitleMinLength),
        Validators.maxLength(RentalAdValidationRules.TitleMaxLength),
      ]),
      description: new FormControl(this.rentalAd.description, [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.DescriptionMinLength),
        Validators.maxLength(RentalAdValidationRules.DescriptionMaxLength),
      ]),
      country: new FormControl(this.rentalAd.placeAddress.country, [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.PlaceAddressMinLength),
        Validators.maxLength(RentalAdValidationRules.PlaceAddressMaxLength),
      ]),
      city: new FormControl(this.rentalAd.placeAddress.city, [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.PlaceAddressMinLength),
        Validators.maxLength(RentalAdValidationRules.PlaceAddressMaxLength),
      ]),
      street: new FormControl(this.rentalAd.placeAddress.street, [
        Validators.required,
        Validators.minLength(RentalAdValidationRules.PlaceAddressMinLength),
        Validators.maxLength(RentalAdValidationRules.PlaceAddressMaxLength),
      ]),
      price: new FormControl(this.rentalAd.pricePerDay, [Validators.required,
      Validators.min(RentalAdValidationRules.PricePerDayMinPrice),
      Validators.max(RentalAdValidationRules.PricePerDayMaxPrice)]),
      file: new FormControl(''),
      fileSource: new FormControl(''),
    });
  }

  get title() { return this.updateForm.get('title'); }
  get description() { return this.updateForm.get('description'); }
  get country() { return this.updateForm.get('country'); }
  get city() { return this.updateForm.get('city'); }
  get street() { return this.updateForm.get('street'); }
  get price() { return this.updateForm.get('price'); }

  public async updateRentalAd(form: FormGroup) {
    if (this.rentalAd.description != form.value.description) {
      await this.rentalAdService.updateRentaldDescription(
        this.rentalAd.rentalAdId,
        form.value.description
      )
    }

    if (this.rentalAd.title != form.value.title) {
      await this.rentalAdService.updateRentaldTitle(
        this.rentalAd.rentalAdId,
        form.value.title
      );
    }

    if (
      this.rentalAd.placeAddress.street != form.value.street ||
      this.rentalAd.placeAddress.city != form.value.city ||
      this.rentalAd.placeAddress.country != form.value.country
    ) {
      await this.rentalAdService.updateRentaldAddress(
        this.rentalAd.rentalAdId,
        form.value.country,
        form.value.city,
        form.value.street
      );
    }

    if (this.rentalAd.pricePerDay != form.value.price) {
      await this.rentalAdService.updateRentaldPrice(
        this.rentalAd.rentalAdId,
        form.value.price
      );
    }

    if (typeof this.uploads !== 'undefined' && this.uploads.length > 0) {
      await this.rentalAdService.updateRentaldPhotos(
        this.rentalAd.rentalAdId,
        Array.from(this.uploads)
      );
    }
  }

  get f() {
    return this.updateForm.controls;
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

          this.updateForm.patchValue({
            fileSource: this.photos,
          });
        };

        reader.readAsDataURL(event.target.files[i]);
      }
    }
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

  public removePhoto(photoId: string, index: number) {
    if (Array.isArray(this.existingPhotos)) {
      this.existingPhotos.splice(index, 1);
    }

    this.rentalAdService.removePhoto(this.rentalAd.rentalAdId, photoId);
  }
}
