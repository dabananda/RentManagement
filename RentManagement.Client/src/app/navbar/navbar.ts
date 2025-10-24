import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Account } from '../_services/account';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-navbar',
  imports: [FormsModule, RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  model: any = {};
  accountService = inject(Account);

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
