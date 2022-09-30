import { CityStats } from './citystats';

export interface CityStatsOverview {
  cityStats: CityStats[];
  averageAirQuality: number;
  averageWaterPollution: number;
  maxAirQuality: number;
  minAirQuality: number;
  maxWaterQuality: number;
  minWaterQuality: number;
  medianWaterQuality: number;
  medianAirQuality: number;
}
