import { Component, inject } from '@angular/core';
import { LoaderService } from '../_services/loader.service';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'app-loader',
  imports: [CommonModule, NgIf],
  templateUrl: './loader.html',
  styleUrl: './loader.css',
})
export class Loader {
  loader = inject(LoaderService);
}
