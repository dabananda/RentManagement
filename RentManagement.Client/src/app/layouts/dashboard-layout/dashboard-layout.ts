import { Component } from '@angular/core';
import { Navbar } from "../../navbar/navbar";
import { RouterOutlet } from "@angular/router";
import { Sidebar } from "../../sidebar/sidebar";

@Component({
  selector: 'app-dashboard-layout',
  imports: [Navbar, RouterOutlet, Sidebar],
  templateUrl: './dashboard-layout.html',
  styleUrl: './dashboard-layout.css'
})
export class DashboardLayout {

}
