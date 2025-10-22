import { Component, inject, OnInit } from '@angular/core';
import { Account } from './_services/account';
import { DashboardLayout } from "./layouts/dashboard-layout/dashboard-layout";

@Component({
  selector: 'app-root',
  imports: [DashboardLayout],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private accountService = inject(Account);

  ngOnInit(): void {
    this.accountService.hydrateFromStore()
  }
}
