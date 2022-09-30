import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { City } from 'src/app/interfaces/city';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  constructor(private httpClient: HttpClient) {}

  city!: string;
  testData!: any;
  cities$!: Observable<City[]>;
  cities!: City[];

  @Output() onCityChange: EventEmitter<string> = new EventEmitter<string>();

  ngOnInit(): void {
    this.cities$ = this.httpClient.get<City[]>('/courses.json');
  }

  getCities() {
    return this.httpClient.get('url');
  }

  handleClick() {
    // this.getCities().subscribe((data: any) => {
    //   this.onCityChange.emit(data);
    // });
  }
}
