import { Component, inject, OnInit } from '@angular/core';
import { RentService } from '../../_services/rent.service';

@Component({
  selector: 'app-rent-list',
  imports: [],
  templateUrl: './rent-list.html',
  styleUrl: './rent-list.css',
})
export class RentList implements OnInit {
  rentService = inject(RentService);

  ngOnInit(): void {
    this.loadRents();
  }

  loadRents() {
    this.rentService.getRents();
  }
}
