import { Component, OnInit } from '@angular/core';
import { FeedService } from '../shared/services/feed.service';
import { FeedInformation } from '../shared/models/feed-information.model';

export class CategoryGroup {
  name: string;
  isExpanded: boolean;
  feedsInfo: Array<FeedInformation>;

  constructor(cat: string, expanded: boolean, feeds: Array<FeedInformation>) {
    this.name = cat;
    this.isExpanded = expanded;
    this.feedsInfo = feeds;
  }
}

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  public categoriesGroup: Array<CategoryGroup>;

  constructor(private feedService: FeedService) { }

  ngOnInit(): void {
    this.loadFeedInformation();

  }

  public collapse(category: CategoryGroup): void {
    category.isExpanded = !category.isExpanded;
  }

  public loadFeedInformation(): void {
    this.feedService.getFeedInformation().subscribe(data => {
      this.categoriesGroup = this.groupFeeds(data);
    });
  }

  public getCategoryPostsCount(category: CategoryGroup): number {
    if (!category) {
      return 0;
    }

    return category.feedsInfo.reduce((prev, current, index) => prev + current.postsCount, 0);
  }

  public getTotalCount(): number {

    if (!this.categoriesGroup) {
      return null;
    }

    let sum: number = 0;

    for (let i = 0; i < this.categoriesGroup.length; i++) {
      sum += this.getCategoryPostsCount(this.categoriesGroup[i]);
    }

    return sum;
  }

  private groupFeeds(feedsInfo: Array<FeedInformation>): Array<CategoryGroup> {
    if (!feedsInfo)
      return null;

    const groupBy: string = "category";

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
}
