import { Component, inject, OnInit, signal } from '@angular/core';
import { DashboardService } from '../_services/dashboard.service';
import { CommonModule } from '@angular/common';
import { CardData } from '../_models/card-data';
import { AgreementTable } from '../_models/agreement-table';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  dashboardService = inject(DashboardService);

  cardData = signal<CardData | null>(null);
  agreementTable = signal<AgreementTable[]>([]);

  ngOnInit() {
    this.loadData();
    this.loadAgreementTable();
  }

  loadData() {
    this.dashboardService.loadCardData().subscribe({
      next: (data) => {
        console.log(data);
        this.cardData.set(data);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  loadAgreementTable() {
    this.dashboardService.loadAgreementTable().subscribe({
      next: (data) => {
        console.log(data);
        this.agreementTable.set(data);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
