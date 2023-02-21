import { AlertifyService } from './../../../../services/alertify.service';
import { Router } from '@angular/router';
import { IdentityService } from './../../../../services/identity.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  
  constructor(private identityService: IdentityService, private route: Router, private alertify: AlertifyService) { }

  ngOnInit() {
  }
  
  public signIn(emailAddress: string, password: string){
    this.identityService.login(emailAddress, password)
    .subscribe((res) => {
      this.alertify.success('logged in successfully');
      this.route.navigate(['']);
    }, error => {
      this.alertify.error('Cannot login, try another credentials');
      this.route.navigate(['login'])
    });
  }
}
