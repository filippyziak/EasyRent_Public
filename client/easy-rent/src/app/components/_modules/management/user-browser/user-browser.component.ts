import { ManagementService } from './../../../../services/management.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { IdentityService } from 'src/app/services/identity.service';

@Component({
  selector: 'app-user-browser',
  templateUrl: './user-browser.component.html',
  styleUrls: ['./user-browser.component.scss'],
})
export class UserBrowserComponent implements OnInit {
  accounts: any;
  constructor(
    private route: ActivatedRoute,
    private managementService: ManagementService,
    private identityService: IdentityService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.accounts = data.accountResolver.accounts.filter(
        (a) => a.accountId != this.identityService.getAccountId()
      );
      console.log(this.accounts);
    });
  }

  public isActive(state: string) {
    return state == 'Active';
  }

  public suspend(account: any) {
    this.managementService.suspend(account.accountId);
    account.state = 'Suspended';
  }

  public activate(account: any) {
    this.managementService.activate(account.accountId);
    account.state = 'Active';
  }
}
