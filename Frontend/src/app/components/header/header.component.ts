import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/shared/services/authentication.service';
import { User } from 'src/shared/models/user.model';

@Component({
  selector: 'olc-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  currentUser: User
  constructor(private router:Router, private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.currentUser = this.authenticationService.currentUserValue
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
    this.router.navigate(['/login']);
  }
}
