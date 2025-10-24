import { Component, inject, OnInit, signal } from '@angular/core';
import { DashboardService } from '../_services/dashboard.service';
import { CommonModule } from '@angular/common';
import { CardData } from '../_models/card-data';
import { AgreementTable } from '../_models/agreement-table';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  dashboardService = inject(DashboardService);

  cardData = signal<CardData | null>(null);
  agreementTable = signal<AgreementTable[]>([]);
  searchQuery = signal<string>('');

  ngOnInit() {
    this.loadData();
    this.loadAgreementTable();
  }

  loadData() {
    this.dashboardService.loadCardData().subscribe({
      next: (data) => {
        this.cardData.set(data);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  loadAgreementTable(search?: string) {
    this.dashboardService.loadAgreementTable(search).subscribe({
      next: (data) => {
        this.agreementTable.set(data);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  onSearch(event: Event) {
    event.preventDefault();
    this.loadAgreementTable(this.searchQuery());
  }

  onSearchInput() {
    if (this.searchQuery().length === 0) {
      this.loadAgreementTable();
    }
  }
}
