/*! Rappid v3.1.1 - HTML5 Diagramming Framework - TRIAL VERSION

Copyright (c) 2015 client IO

 2020-05-17 


This Source Code Form is subject to the terms of the Rappid Trial License
, v. 2.0. If a copy of the Rappid License was not distributed with this
file, You can obtain one at http://jointjs.com/license/rappid_v2.txt
 or from the Rappid archive as was distributed by client IO. See the LICENSE file.*/


import * as joint from '../../vendor/rappid';
import { memoize } from 'lodash';
import { stringify } from 'querystring';

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

    export class HangUp extends joint.dia.Element{
        defaults(){
            return joint.util.defaultsDeep({
             
               options:'normal',
               type: 'app.HangUp',
               markup: `<g><rect class="body"/>
               <text class="widgetName"></text>
               <text class="widgetTitle" ></text>
               <image style="color:#6d6e71"/>
               </g>`
                ,
                attrs: {
                    '.': {
                        magnet: false,
                    },
                    '.body': {
                        refWidth: '100%',
                        refHeight: '100%',
                        refX: 0,
                        refY: 0,
                        fill: '#fff',
                        // filter: {
                        //     name: 'dropShadow',
                        //     args: {
                        //         dx: 0,
                        //         dy: 0,
                        //         blur: 3
                        //     }
                        // },
                        stroke: 0
                    },
                    '.widgetName': {
                        refX: 15,
                        refY: 15,
                        fontSize: 13,
                        fill: '#6d6e71',
                        fontFamily: 'ArialMT, Arial',
                        text: 'Hanup'
                    },
                    '.widgetTitle': {
      
                        refY: '40',
                        refX: '50%',
                        textAnchor: 'middle',
                         
                        fontSize: 15,
                        fill:'#58595b',
                        fontFamily: 'ArialMT, Arial',
                        fontWeight:700,
                        text: 'Hang Up'
                    },
                    image: {
                        xlinkHref: 'assets/phone-volume-solid-gray.svg',
                        fill: '#58595b',
                        height: '25',
                        width: '25',
                        refY: 35,
                        refX: 14
            
                    },
                    '.widgetDescription': {
                        refX: 15,
                        refY: 80,
                        fontSize: 14,
                        fill:'#58595b',
                        fontFamily: 'ArialMT, Arial',
                        text: 'Dial 0422613824'
                    },
                    '.timeoutRect': {
                        refWidth: '93%',
                        height: '28',
                        refX: 10,
                        refY: '70%',
                        fill: '#26A79F'
                    },
                    '.timeoutText': {
                        ref: '.timeoutRect',
                        fontSize: 14,
                        fill:'#fff',
                        fontFamily: 'ArialMT, Arial',
                        fontWeight:700,
                        refX: '50%',
                        refY: 19,
                        textAnchor: 'middle'
                    }
                },
               ports: {
                items: [
                       { group: 'in', id: 'in' },
                    //   { group: 'out' },
                    // { group: 'out2' }
                ],
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
                                fill: '#308AD0',
                                strokeWidth: 0
                            },
                            portLabel: {
                                fontSize: 11,
                                fill: '#308AD0',
                                fontWeight: 800
                            }
                        },
                        position: {
                            name: 'ellipse',
                            // args: {
                            //     startAngle: 0,
                            //     step: 30
                            // }
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
                    
            }, joint.dia.Element.prototype.defaults);
        }
    }

    export class IncomingCall extends joint.dia.Element{
        validateProperty(path, value){
            switch (path) {
                case 'attrs/.widgetTitle/text':
                    if (value == ''){
                        return "Please enter a name";
                    }
            }
            return null;
        }

        defaults(){
            
            return joint.util.defaultsDeep({
                defaultWhisper: '',
                ringingTimeOut: 10,
                enableCallerIdPassthrough: false,
                type: 'app.IncomingCall',
               markup: `<g><rect class="body"/>
               <text class="widgetName"></text>
               <text class="widgetTitle" ></text>
               <image style="color:#6d6e71"/></g>`
                ,
                attrs: {
                    '.': {
                        magnet: false,
                    },
                    '.body': {
                        refWidth: '100%',
                        refHeight: '100%',
                        refX: 0,
                        refY: 0,
                        fill: '#fff',
                        stroke: 0
                    },
                    '.widgetName': {
                        refX: 15,
                        refY: 15,
                        fontSize: 13,
                        fill: '#6d6e71',
                        fontFamily: 'ArialMT, Arial',
                        text: 'Incoming Call'
                    },
                    '.widgetTitle': {
      
                        refY: '40',
                        refX: '50%',
                         textAnchor: 'middle',
                         
                        fontSize: 15,
                        fill:'#58595b',
                        fontFamily: 'ArialMT, Arial',
                        fontWeight:700,
                        text: 'Default Call Flow'
                    },
                    image: {
                        xlinkHref: 'assets/phone-volume-solid-gray.svg',
                        fill: '#58595b',
                        height: '25',
                        width: '25',
                        refY: 35,
                        refX: 14
            
                    },
                    '.widgetDescription': {
                        refX: 15,
                        refY: 80,
                        fontSize: 14,
                        fill:'#58595b',
                        fontFamily: 'ArialMT, Arial',
                        text: 'Dial 0422613824'
                    }
                },
               ports: {
                items: [
                      { group:'out',id:'success' },
                ],
                groups: {
                 
                    'out': {
                        markup: `<g class="outputPort">
                        <rect class="timeoutRect"  />
                        <text class="timeoutText" ><tspan x="7.11" y="0">Call Connected</tspan></text>
                        </g>`,
                        attrs: {
                            '.outputPort' :{
                                magnet: true
                            },
                            '.timeoutRect': {
                                width: '270',
                                height: '28',
                                fill: '#26A79F',
                            },
                            '.timeoutText': {
                                ref: '.timeoutRect',
                                fontSize: 14,
                                fill:'#fff',
                                fontFamily: 'ArialMT, Arial',
                                fontWeight:700,
                                refX: '50%',
                                refY: 19,
                                textAnchor: 'middle'
                            },
                        },
                        position: {
                            name: 'bottom',
                            args:{x:10,y:80}
                        }
                    }
                }
            }
                    
            }, joint.dia.Element.prototype.defaults);
        }
    }

    export class Dial extends joint.dia.Element{
        // number: string;
        defaults(){
            return joint.util.defaultsDeep({
                // name: '',
                playSound: '',
                whisper: '',
                dialOptions: 'dialNumber',
                // number: '0422613824',

                postcodeMapperProfile: '',
                store: '',
                usePrompt: true,
                ringingTimeOut: 20,
                musicOnHold: '',

                type: 'app.Dial',
               markup: `<g><rect class="body"/>
               <text class="widgetName"></text>
               <text class="widgetTitle" ></text>
               <image style="color:#6d6e71"/>
               <text class="widgetDescription"></text>
               </g>`
                ,
                attrs: {
                    '.': {
                        magnet: false,
                    },
                    '.body': {
                        refWidth: '100%',
                        refHeight: '100%',
                        refX: 0,
                        refY: 0,
                        fill: '#fff',
                        // filter: {
                        //     name: 'dropShadow',
                        //     args: {
                        //         dx: 0,
                        //         dy: 0,
                        //         blur: 3
                        //     }
                        // },
                        stroke: 0
                    },
                    '.widgetName': {
                        refX: 15,
                        refY: 15,
                        fontSize: 13,
                        fill: '#6d6e71',
                        fontFamily: 'ArialMT, Arial',
                        text: 'Dial'
                    },
                    '.widgetTitle': {
      
                        refY: '20%',
                        refX: '50%',
                        textAnchor: 'middle',
                        fontSize: 15,
                        fill:'#58595b',
                        fontFamily: 'ArialMT, Arial',
                        fontWeight:700,
                        text: 'Sales'
                    },
                    image: {
                        xlinkHref: 'assets/phone-volume-solid-gray.svg',
                          fill: '#58595b',
                          height: '25',
                          width: '25',
                        refY: 35,
                        refX: 9
            
                    },
                    '.widgetDescription': {
                        refX: 15,
                        refY: 80,
                        fontSize: 14,
                        fill:'#58595b',
                        fontFamily: 'ArialMT, Arial'
                    }
                },
               ports: {
                items: [
                      { group: 'in', id:'in' },
                      { group: 'out', id:'timeout' },
                    // { group: 'out2' }
                ],
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
                                fill: '#308AD0',
                                strokeWidth: 0
                            },
                            portLabel: {
                                fontSize: 11,
                                fill: '#308AD0',
                                fontWeight: 800
                            }
                        },
                        position: {
                            name: 'ellipse',
                            // args: {
                            //     startAngle: 0,
                            //     step: 30
                            // }
                        },
                        label: {
                            position: {
                                name: 'radial',
                                args: null
                            }
                        }
                    },
                    'out': {
                        markup: `<g class="outputPort">
                        <rect class="timeoutRect"  />
                        <text class="timeoutText" ><tspan x="7.11" y="0">Timeout</tspan></text>
                        </g>`,
                        attrs: {
                            '.outputPort' :{
                                magnet: true
                            },
                            '.timeoutRect': {
                                width: '270',
                                height: '28',
                                fill: '#26A79F',
                            },
                            '.timeoutText': {
                                ref: '.timeoutRect',
                                fontSize: 14,
                                fill:'#fff',
                                fontFamily: 'ArialMT, Arial',
                                fontWeight:700,
                                refX: '50%',
                                refY: 19,
                                textAnchor: 'middle'
                            },
                        },
                        position: {
                            name: 'bottom',
                            args:{x:10,y:150}
                        }
                    }
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
                    name: 'orthogonal'
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

