import * as joint from '../../vendor/rappid';
import { app } from 'src/shapes/app-shapes';

export class Factory {
    createIncomingCallElement(){
        return new app.Incomingcall({
            size: { width: 130, height: 50 },
            position: { x: 400, y: 100 },
        });
        return new joint.shapes.standard.Rectangle({
            size: { width: 100, height: 50 },
            position: { x: 400, y: 100 },
            attrs: {
                body: {
                    rx: 2,
                    ry: 2,
                    width: 50,
                    height: 30,
                    fill: 'transparent',
                    stroke: '#31d0c6',
                    strokeWidth: 2,
                    strokeDasharray: '0'
                },
                label: {
                    text: 'Incoming Call',
                    fill: '#c6c7e2',
                    fontFamily: 'Roboto Condensed',
                    fontWeight: 'Normal',
                    fontSize: 11,
                    strokeWidth: 0
                }
            },
            // outPorts: ['out'],
            ports: {
                items: [
                    // { group: 'in' },
                    // { group: 'in' },
                    { group: 'out' }
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
        });
    }
}