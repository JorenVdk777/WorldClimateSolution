import { Component, OnInit } from '@angular/core';
import { ApiCallerService } from 'src/app/services/api-caller.service';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.scss'],
})
export class ChartsComponent implements OnInit {
  waterQuality: number[] = [];
  columnChartOptions = {
    animationEnabled: true,
    title: {
      text: 'City eco',
    },
    data: [
      {
        // Change type to "doughnut", "line", "splineArea", etc.
        type: 'column',
        dataPoints: [
          { label: 'apple', y: 10 },
          { label: 'orange', y: 15 },
          { label: 'banana', y: 25 },
          { label: 'mango', y: 30 },
          { label: 'grape', y: 28 },
        ],
      },
    ],
  };

  pieChartOptions = {
    animationEnabled: true,
    title: {
      text: 'City eco but in pie chart',
    },
    theme: 'light2', // "light1", "dark1", "dark2"
    data: [
      {
        type: 'pie',
        dataPoints: [
          { label: 'apple', y: 10 },
          { label: 'orange', y: 15 },
          { label: 'banana', y: 25 },
          { label: 'mango', y: 30 },
          { label: 'grape', y: 28 },
        ],
      },
    ],
  };

  lineChartOptions = {
    animationEnabled: true,
    title: {
      text: 'City eco but in lines please send help',
    },
    theme: 'light2', // "light1", "dark1", "dark2"
    data: [
      {
        type: 'line',
        dataPoints: [
          { label: 'apple', y: 10 },
          { label: 'orange', y: 15 },
          { label: 'banana', y: 25 },
          { label: 'mango', y: 30 },
          { label: 'grape', y: 28 },
        ],
      },
    ],
  };

  constructor(private apiService: ApiCallerService) {
    apiService.apiData.subscribe((value) => {
      const waterQualityWashington = value.cityStats.find((v) =>
        v.city.includes('Washington')
      );
      console.log(waterQualityWashington);
      // this.waterQuality = value.cityStats.map((v) => v.waterQuality);
      // console.log(value);
      // console.log(this.waterQuality);

      const tempOptions = this.columnChartOptions;
      tempOptions.data = [
        {
          // Change type to "doughnut", "line", "splineArea", etc.
          type: 'column',
          dataPoints: [
            {
              label: waterQualityWashington!.city,
              y: waterQualityWashington!.waterQuality,
            },
          ],
        },
      ];
      this.columnChartOptions = tempOptions;
    });
  }

  ngOnInit(): void {}
}
