import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Account } from '../_services/account';

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css',
})
export class Sidebar {
  accountService = inject(Account);
}
