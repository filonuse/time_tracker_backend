
// Action wich calls when item of menu is click.
export function onMenuItemClick(event, hasChildren, elementHeight, childElementsHeight) {
    const additionalClassName = hasChildren ? ' expanded' : ' active';
    const element = event.target.closest('li');
    const isOpened = element.className.match(/\sexpanded/) ? true : false;

    !hasChildren && elementCollapsing('active');
    elementCollapsing('expanded', elementHeight)
    if (!isOpened && hasChildren) {
        element.className += additionalClassName;
        element.style.height = childElementsHeight + elementHeight + 'px';
    }
    else if (!isOpened) {
        element.className += additionalClassName
    }
}

// Action wich calls when item of submenu is click.
export function onSubmenuItemClick(event) {
    const additionalClassName = ' active';
    const element = event.target.closest('li');
    const elements = [
        element,
        element.parentElement.closest('li'),
        element.firstChild
    ];

    elementCollapsing('active')
    elements.forEach(element => element.className += additionalClassName);
}

// Additional action. It calls expanding of menu items.
export function elementExpanding() {
    const element = document.getElementById('main-menu').querySelector('li.active');
    const hasChildren = element.querySelector('ul').childNodes.length ? true : false;
    const isOpened = element.className.match(/\sexpanded/) ? true : false;

    // Exit if function was opened or doesn`t have children.
    if (!hasChildren || isOpened) { return }

    const elementHeight = element.clientHeight;
    const children = element.querySelector('ul').childNodes;
    const childAmount = children.length;
    const childHeight = children[0].clientHeight;
    const childElementsHeight = childAmount * childHeight;

    element.className += ' expanded';
    element.style.height = childElementsHeight + elementHeight + 'px';
}

// Additional action. It calls collapsing of menu items.
export function elementCollapsing(className, elementHeight = 40) {
    switch (className) {
        case 'expanded':
            return document.getElementById('main-menu').querySelectorAll('li.expanded').forEach(element => {
                element.style.height = elementHeight + 'px';
                element.className = element.className.replace(/\sexpanded/, '');
            });
        case 'active':
            return document.getElementById('main-menu').querySelectorAll('.active').forEach(element => (
                element.className = element.className.replace(/\sactive/, '')
            ));
    }
}

export function onMouseEvent(expanded = false, toggleElement, isExpanded) {
    if (!expanded) {
        toggleElement.className = 'menubar-visible';
        isExpanded = true;
        elementExpanding();
    }
    else {
        toggleElement.className = '';
        isExpanded = false;
        elementCollapsing('expanded');
    }
}
