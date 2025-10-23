import { Component, inject, OnInit } from '@angular/core';
import { Account } from './_services/account';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private accountService = inject(Account);

  ngOnInit(): void {
    this.accountService.hydrateFromStore()
  }
}
