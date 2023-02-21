import { IdentityService } from './../../../../services/identity.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss'],
})
export class EditProfileComponent implements OnInit {
  constructor(private identityService: IdentityService) {}

  ngOnInit() {}

  public updateEmail(email: string) {
    this.identityService.updateEmailAddress(email);
  }

  public updatePassword(password: string) {
    this.identityService.updatePassword(password);
  }
}
