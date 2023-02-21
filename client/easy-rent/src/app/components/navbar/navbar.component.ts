import { IdentityService } from './../../services/identity.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(public identityService: IdentityService) { }

  ngOnInit() {
  }
}
