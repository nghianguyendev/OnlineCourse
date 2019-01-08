import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/shared/services/authentication.service';

@Component({
  selector: 'olc-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private router:Router, private authenticationService: AuthenticationService) { }

  ngOnInit() {
  }

  goHome(){
    this.router.navigate[''];
  }

  goCourse(){
    this.router.navigate['course'];
    alert('go course');
  }

  logout(){
    this.authenticationService.logout();    
    this.router.navigate['login'];
  }
}
