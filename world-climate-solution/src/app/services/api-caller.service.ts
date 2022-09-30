import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { CityStats } from '../interfaces/citystats';
import { CityStatsOverview } from '../interfaces/citystatsoverview';

@Injectable({
  providedIn: 'root',
})
export class ApiCallerService {
  constructor(private httpClient: HttpClient) {}

  getData() {
    return this.httpClient.get<CityStatsOverview>(
      'http://localhost:7132/City-Stats/Overview'
    );
  }
}
