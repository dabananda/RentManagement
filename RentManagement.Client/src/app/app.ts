import { Component, inject, OnInit } from '@angular/core';
import { Account } from './_services/account';
import { RouterOutlet } from '@angular/router';
import { Loader } from './loader/loader';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Loader],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private accountService = inject(Account);

  ngOnInit(): void {
    this.accountService.hydrateFromStore();
  }
}
