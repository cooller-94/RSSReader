import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { User } from '../shared/models/user.model';
import { AuthService } from '../auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'top-nav-menu',
  templateUrl: './top-nav-menu.component.html',
  styleUrls: ['./top-nav-menu.component.css']
})
export class TopNavMenuComponent implements OnInit {

  public currentUser: User;

  constructor(private userService: UserService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.loadCurrentUser();
  }

  public onLogOut(): void {
    this.authService.logout();
    this.router.navigate(["/login"]);
  }

  private loadCurrentUser(): void {
    this.userService.getUser().subscribe(user => {

      if (user) {
        this.currentUser = user;
      }
    });
  }
}
