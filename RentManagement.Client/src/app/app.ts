import { Component, inject, OnInit } from '@angular/core';
import { Navbar } from './navbar/navbar';
import { Account } from './_services/account';
import { RouterOutlet } from '@angular/router';
import { TokenService } from './_services/token.service';

@Component({
  selector: 'app-root',
  imports: [Navbar, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private accountService = inject(Account);

  ngOnInit(): void {
    this.accountService.hydrateFromStore()
  }
}
