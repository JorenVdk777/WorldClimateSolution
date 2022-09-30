import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { CityStatsOverview } from 'src/app/interfaces/citystatsoverview';
import { ApiCallerService } from 'src/app/services/api-caller.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  constructor(private apiService: ApiCallerService) {}

  city!: string;
  testData!: any;

  @Output() onCityChange: EventEmitter<string> = new EventEmitter<string>();

  ngOnInit(): void {}

  getCities() {}

  handleClick() {
    this.apiService.getData().subscribe((value) => {
      this.apiService.apiData.next(value);
    });
  }
}
