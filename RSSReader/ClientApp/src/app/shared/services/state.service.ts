import { Injectable } from "@angular/core";
import { CategoryGroup } from "../models/category-group.model";
import { FeedService } from "./feed.service";
import { FeedInformation } from "../models/feed-information.model";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { SyncFeedService } from "./sync-feed.service";
import { SyncFeedResult } from "../models/sync-feed-result.model";

@Injectable()
export class StateService {

  constructor(private feedService: FeedService, private syncService: SyncFeedService) { }

  public groupCategoriesBehaviorSubject: BehaviorSubject<CategoryGroup[]> = new BehaviorSubject<CategoryGroup[]>(null);

  private syncInterval: any;
  private syncIntervalTime: number = 1000 * 60; //update ever one minute

  public init(feeds: FeedInformation[]): void {
    let categories = this.groupFeeds(feeds, "category");

    this.groupCategoriesBehaviorSubject.next(categories);
    //this.initSyncInterval();
  }

  public totalCountUnreadPosts(): number {

    let categoriesGroup = this.groupCategoriesBehaviorSubject.value;

    if (!categoriesGroup) {
      return 0;
    }

    let sum: number = 0;

    for (let i = 0; i < categoriesGroup.length; i++) {
      sum += this.countUnreadPostsByCategory(categoriesGroup[i]);
    }

    return sum;
  }

  public markAsRead(category: string, feedId: number): void {
    let categoryGroup = this.groupCategoriesBehaviorSubject.value.find(i => i.name === category);

    if (categoryGroup) {
      let feed = categoryGroup.feedsInfo.find(i => i.feedId === feedId);

      if (feed) {
        feed.postsCount--;
      }
    }
  }

  public countUnreadPostsByCategory(category: CategoryGroup): number {
    if (!category) {
      return 0;
    }

    return category.feedsInfo.reduce((prev, current, index) => prev + current.postsCount, 0);
  }

  private groupFeeds(feedsInfo: Array<FeedInformation>, groupBy: string): Array<CategoryGroup> {
    if (!feedsInfo)
      return null;

    const groupedCollection = feedsInfo.reduce((previous, current) => {
      if (!previous[current[groupBy]]) {
        previous[current[groupBy]] = [current];
      } else {
        previous[current[groupBy]].push(current);
      }

      return previous;
    }, {});

    return Object.keys(groupedCollection).map(key => new CategoryGroup(key, false, groupedCollection[key]));
  }

  private initSyncInterval(): void {
    let self = this;
    console.log("Init sync")
    this.syncInterval = window.setInterval(function () {
      console.log("Updating...");
      self.syncService.syncAllFeeds().subscribe(data => {
        if (data && data.length > 0) {
          this.updateCategoryGroup(data);
          console.log("End of the updating: ", data);
        }
      });
    }, self.syncIntervalTime)
  }

  private updateCategoryGroup(syncResults: SyncFeedResult[]): void {
    for (let i = 0; i < syncResults.length; i++) {
      this.addCountToFeed(syncResults[i].categoryTitle, syncResults[i].feedId, syncResults[i].postsCount);
    }
  }

  private addCountToFeed(category: string, feedId: number, count: number): void {
    let categoryGroup = this.groupCategoriesBehaviorSubject.value.find(i => i.name === category);

    if (category) {
      let feed = categoryGroup.feedsInfo.find(i => i.feedId === feedId);

      if (feed) {
        feed.postsCount += count;
      }
    }
  }

  private destroySyncInterval(): void {
    window.clearInterval(this.syncIntervalTime);
  }

}
