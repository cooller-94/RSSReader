import { Component, OnInit, OnDestroy } from '@angular/core';
import { FeedService } from '../shared/services/feed.service';
import { CategoryGroup } from '../shared/models/category-group.model';
import { StateService } from '../shared/services/state.service';
import { Subscription } from 'rxjs/Subscription';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy {

  public categoriesGroup: Array<CategoryGroup>;

  private categoriesGroupSubscription: Subscription;

  constructor(private feedService: FeedService, private stateService: StateService, private router: Router) { }

  ngOnInit(): void {
    this.loadFeedInformation();

    this.categoriesGroupSubscription = this.stateService.groupCategoriesBehaviorSubject.subscribe(data => {

      if (data) {
        this.categoriesGroup = data;
      }
    });
  }

  ngOnDestroy(): void {
    this.categoriesGroupSubscription.unsubscribe();
  }

  public collapse(category: CategoryGroup): void {
    category.isExpanded = !category.isExpanded;
  }

  public loadFeedInformation(): void {
    this.feedService.getFeedInformation().subscribe(data => {
      this.stateService.init(data);
    });
  }

  public getCategoryPostsCount(category: CategoryGroup): number {
    return this.stateService.countUnreadPostsByCategory(category);
  }

  public getTotalCount(): number {
    return this.stateService.totalCountUnreadPosts();
  }

  public navigateToCategory(name: string): void {
    this.router.navigate(['categories', name]);
  }
}
