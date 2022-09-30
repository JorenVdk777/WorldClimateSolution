import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  constructor() {}

  city!: string;

  @Output() onCityChange: EventEmitter<string> = new EventEmitter<string>();

  ngOnInit(): void {}

  handleClick() {
    this.onCityChange.emit(this.city);
  }
}
