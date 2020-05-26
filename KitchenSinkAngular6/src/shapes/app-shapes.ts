/*! Rappid v3.1.1 - HTML5 Diagramming Framework - TRIAL VERSION

Copyright (c) 2015 client IO

 2020-05-17 


This Source Code Form is subject to the terms of the Rappid Trial License
, v. 2.0. If a copy of the Rappid License was not distributed with this
file, You can obtain one at http://jointjs.com/license/rappid_v2.txt
 or from the Rappid archive as was distributed by client IO. See the LICENSE file.*/


import * as joint from '../../vendor/rappid';
import { memoize } from 'lodash';

export namespace app {

    export class StencilItem extends joint.shapes.standard.InscribedImage{
        defaults(){
            return joint.util.defaultsDeep({
                type: 'app.StencilItem',
                 size: { width: 60, height: 60 },

                attrs: {
                    border: {
                        stroke: '#31d0c6',
                        strokeWidth: 3,
                        strokeDasharray: '0'
                    },
                    background: {
                        fill: 'transparent'
                    },
                    image: {
                        xlinkHref: 'assets/image-icon1.svg',
                        fill: '#30d0c6'
                    },
                    label: {
                        // text: 'icon',
                        fill: '#c6c7e2',
                        fontFamily: 'Roboto Condensed',
                        fontWeight: 'Normal',
                        fontSize: 11,
                        strokeWidth: 0
                    },
                    
                }
                    
            }, joint.shapes.standard.InscribedImage.prototype.defaults);
        }
    }

    export class Incomingcall extends joint.dia.Element{
        defaults(){
            return joint.util.defaultsDeep({
                name: 'Name 1',
                type: 'app.IncomingCall',
                //  size: { width: 120, height: 120 },
                markup: `<rect class="body"/><text class="question-text"/>
                <g class="option"></g>
               `,
                ports: {
                    groups: {
                        'in': {
                            position: 'top',
                            attrs: {
                                circle: {
                                    magnet: 'passive',
                                    stroke: 'white',
                                    fill: '#feb663',
                                    r: 14
                                },
                                text: {
                                    pointerEvents: 'none',
                                    fontSize: 12,
                                    fill: 'white'
                                }
                            },
                            label: {
                                position: {
                                    name: 'left',
                                    args: { x: 5 }
                                }
                            }
                        },
                        out: {
                            // position: 'bottom',
                            position: {
                                 name: 'bottom',
                                // args: {
                                //     startAngle: 180,
                                //     step: 30
                                // }
                            },
                            // markup:{
                            //     tagName: 'image',
                            //     selector:'image'
                                
                            // },

                             markup:'<g class="root"><image class="image"></image></g>',
                            // markup: ''
                            attrs: {
                                ".root": {
                                    magnet: true,
                                    //  stroke: '#fff',
                                    //  fill: '#0069d9',
                                    //  strokeWidth: 1,
                                    //  xlinkHref: 'assets/angle-double-down-solid.svg',
                             
                                    //  stroke: '#fff',
                                    //  fill: '#0069d9',
                                    //  color: '#0069d9',
                                },
                                ".image": {
                                    refWidth: '25',
                                    refHeight: '25',
                                    refX: '-10',
                                    refY: '-10',
                                    // refWidth: '100%',
                                    // refHeight: '100%',
                                    // magnet: true,
                                    // fill: 'currentColor',
                                    //  strokeWidth: 1,
                                     xlinkHref: 'assets/angle-double-down-solid.svg',
                                    //  stroke: '#fff',
                                    //  color: '#0069d9',
                                    //  strokeWidth: 1
                                    //  refWidth: '100%',
                                    //  refHeight: '100%'
                                
                                }
                            }
                        }
                    },
                    items: [{
                        group: 'out',
                    }
                    // ,{
                    //     group: 'out',args:{x:0}
                    // }
                ]
                },
                attrs: {
                    '.': {
                        magnet: false
                    },
                    '.body': {
                        refWidth: '100%',
                        refHeight: '100%',
                        rx: '1%',
                        // ry: '2%',
                        stroke: 'none',
                        fill: {
                            type: 'linearGradient',
                            stops: [
                                { offset: '0%', color: '#30D0C6' },
                                { offset: '100%', color: '#308AD0' }
                            ],
                            // Top-to-bottom gradient.
                            attrs: { x1: '0%', y1: '0%', x2: '0%', y2: '100%' }
                        }
                    },
                    '.btn-add-option': {
                        refX: 10,
                        refDy: -22,
                        cursor: 'pointer',
                        fill: 'white'
                    },
                    '.btn-remove-option': {
                        xAlignment: 10,
                        yAlignment: 13,
                        cursor: 'pointer',
                        fill: 'white'
                    },
                    '.options': {
                        refX: 0
                    },
            
                    // Text styling.
                    text: {
                        fontFamily: 'Arial'
                    },
                    '.option-text': {
                        fontSize: 11,
                        fill: '#4b4a67',
                        refX: 30,
                        yAlignment: 'middle'
                    },
                    '.question-text': {
                        text: 'Incoming Call',
                        fill: 'white',
                        refX: '50%',
                        refY: 15,
                        fontSize: 15,
                        textAnchor: 'middle',
                        style: {
                            textShadow: '1px 1px 0px gray'
                        }
                    },
            
                    // Options styling.
                    '.option-rect': {
                        rx: 3,
                        ry: 3,
                        stroke: 'white',
                        strokeWidth: 1,
                        strokeOpacity: .5,
                        fillOpacity: .5,
                        fill: 'white',
                        refWidth: '100%'
                    }
                }
                    
            }, joint.dia.Element.prototype.defaults);
        }
    }

    export class DialModel extends joint.shapes.standard.InscribedImage{
        
        defaults() {

            return joint.util.defaultsDeep({
                type: 'app.DialModel',
                icon: 'assets/image-icon1.svg',
                displayName : 'Dial',
                attrs: {
                    root: {
                        dataTooltip: 'Dial',
                        dataTooltipPosition: 'left',
                        dataTooltipPositionSelector: '.joint-stencil'
                    },
                    border: {
                        stroke: '#31d0c6',
                        strokeWidth: 3,
                        strokeDasharray: '0'
                    },
                    background: {
                        fill: 'transparent'
                    },
                    image: {
                        xlinkHref: 'assets/image-icon1.svg'
                    },
                    label: {
                        // text: 'icon',
                        fill: '#c6c7e2',
                        fontFamily: 'Roboto Condensed',
                        fontWeight: 'Normal',
                        fontSize: 11,
                        strokeWidth: 0
                    }
                }
                    
            }, joint.shapes.standard.InscribedImage.prototype.defaults);
        }
    }

    export class CircularModel extends joint.shapes.standard.Ellipse {

        portLabelMarkup = [{
            tagName: 'text',
            selector: 'portLabel'
        }];

        defaults() {

            return joint.util.defaultsDeep({
                type: 'app.CircularModel',
                icon: 'assets/image-icon1.svg',
                displayName : 'Dial',
                attrs: {
                    root: {
                        magnet: false
                    }
                },
                ports: {
                    groups: {
                        'in': {
                            markup: [{
                                tagName: 'circle',
                                selector: 'portBody',
                                attributes: {
                                    'r': 10
                                }
                            }],
                            attrs: {
                                portBody: {
                                    magnet: true,
                                    fill: '#61549c',
                                    strokeWidth: 0
                                },
                                portLabel: {
                                    fontSize: 11,
                                    fill: '#61549c',
                                    fontWeight: 800
                                }
                            },
                            position: {
                                name: 'ellipse',
                                args: {
                                    startAngle: 0,
                                    step: 30
                                }
                            },
                            label: {
                                position: {
                                    name: 'radial',
                                    args: null
                                }
                            }
                        },
                        'out': {
                            markup: [{
                                tagName: 'circle',
                                selector: 'portBody',
                                attributes: {
                                    'r': 10
                                }
                            }],
                            attrs: {
                                portBody: {
                                    magnet: true,
                                    fill: '#61549c',
                                    strokeWidth: 0
                                },
                                portLabel: {
                                    fontSize: 11,
                                    fill: '#61549c',
                                    fontWeight: 800
                                }
                            },
                            position: {
                                name: 'ellipse',
                                args: {
                                    startAngle: 180,
                                    step: 30
                                }
                            },
                            label: {
                                position: {
                                    name: 'radial',
                                    args: null
                                }
                            }
                        }
                    }
                }
            }, joint.shapes.standard.Ellipse.prototype.defaults);
        }
    }

    export class RectangularModel extends joint.shapes.standard.Rectangle {

        portLabelMarkup = [{
            tagName: 'text',
            selector: 'portLabel'
        }];

        defaults() {

            return joint.util.defaultsDeep({
                type: 'app.RectangularModel',
                attrs: {
                    root: {
                        magnet: false
                    }
                },
                ports: {
                    groups: {
                        'in': {
                            markup: [{
                                tagName: 'circle',
                                selector: 'portBody',
                                attributes: {
                                    'r': 10
                                }
                            }],
                            attrs: {
                                portBody: {
                                    magnet: true,
                                    fill: '#61549c',
                                    strokeWidth: 0
                                },
                                portLabel: {
                                    fontSize: 11,
                                    fill: '#61549c',
                                    fontWeight: 800
                                }
                            },
                            position: {
                                name: 'left'
                            },
                            label: {
                                position: {
                                    name: 'left',
                                    args: {
                                        y: 0
                                    }
                                }
                            }
                        },
                        'out': {
                            markup: [{
                                tagName: 'circle',
                                selector: 'portBody',
                                attributes: {
                                    'r': 10
                                }
                            }],
                            position: {
                                name: 'right'
                            },
                            attrs: {
                                portBody: {
                                    magnet: true,
                                    fill: '#61549c',
                                    strokeWidth: 0
                                },
                                portLabel: {
                                    fontSize: 11,
                                    fill: '#61549c',
                                    fontWeight: 800
                                }
                            },
                            label: {
                                position: {
                                    name: 'right',
                                    args: {
                                        y: 0
                                    }
                                }
                            }
                        }
                    }
                }
            }, joint.shapes.standard.Rectangle.prototype.defaults);
        }
    }

    export class Link extends joint.shapes.standard.Link {

        defaults() {
            return joint.util.defaultsDeep({
                type: 'app.Link',
                router: {
                    name: 'normal'
                },
                connector: {
                    name: 'rounded'
                },
                labels: [],
                attrs: {
                    line: {
                        stroke: '#8f8f8f',
                        strokeDasharray: '0',
                        strokeWidth: 2,
                        fill: 'none',
                        sourceMarker: {
                            type: 'path',
                            d: 'M 0 0 0 0',
                            stroke: 'none'
                        },
                        targetMarker: {
                            type: 'path',
                            d: 'M 0 -5 -10 0 0 5 z',
                            stroke: 'none'
                        }
                    }
                }
            }, joint.shapes.standard.Link.prototype.defaults);
        }

        defaultLabel = {
            attrs: {
                rect: {
                    fill: '#ffffff',
                    stroke: '#8f8f8f',
                    strokeWidth: 1,
                    refWidth: 10,
                    refHeight: 10,
                    refX: -5,
                    refY: -5
                }
            }
        };

        getMarkerWidth(type: any) {
            const d = (type === 'source') ? this.attr('line/sourceMarker/d') : this.attr('line/targetMarker/d');
            return this.getDataWidth(d);
        }

        getDataWidth = memoize(function (d: any) {
            return (new joint.g.Path(d)).bbox().width;
        });

        static connectionPoint(line: any, view: any, magnet: any, opt: any, type: any, linkView: any): joint.connectionPoints.GenericConnectionPoint<'boundary'> {
            const markerWidth = linkView.model.getMarkerWidth(type);
            opt = { offset: markerWidth, stroke: true };
            // connection point for UML shapes lies on the root group containg all the shapes components
            const modelType = view.model.get('type');
            if (modelType.indexOf('uml') === 0) opt.selector = 'root';
            // taking the border stroke-width into account
            if (modelType === 'standard.InscribedImage') opt.selector = 'border';
            return joint.connectionPoints.boundary.call(this, line, view, magnet, opt, type, linkView);
        }
    }
}

export const NavigatorElementView = joint.dia.ElementView.extend({

    body: null,

    markup: [{
        tagName: 'rect',
        selector: 'body',
        attributes: {
            'fill': '#31d0c6'
        }
    }],

    presentationAttributes: {
        position: ['TRANSLATE'],
        size: ['RESIZE'],
        angle: ['ROTATE']
    },

    render: function() {
        const { fragment, selectors: { body }} = joint.util.parseDOMJSON(this.markup);
        this.body = body;
        this.el.appendChild(fragment);
        this.updateNodesAttributes();
        this.updateTransformation();
    },

    updateNodesAttributes: function() {
        const { width, height } = this.model.get('size');
        this.body.setAttribute('width', width);
        this.body.setAttribute('height', height);
    }
});


export const NavigatorLinkView = joint.dia.LinkView.extend({

    initialize: joint.util.noop,

    render: joint.util.noop,

    update: joint.util.noop
});

// re-export build-in shapes from rappid
export const basic = joint.shapes.basic;
export const standard = joint.shapes.standard;
export const fsa = joint.shapes.fsa;
export const pn = joint.shapes.pn;
export const erd = joint.shapes.erd;
export const uml = joint.shapes.uml;
export const org = joint.shapes.org;

